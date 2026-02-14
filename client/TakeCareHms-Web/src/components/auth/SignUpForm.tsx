import { useState } from "react";
import { Link, useNavigate } from "react-router";
import { ChevronLeftIcon, EyeCloseIcon, EyeIcon } from "../../icons";
import Label from "../form/Label";
import Input from "../form/input/InputField";
import Checkbox from "../form/input/Checkbox";
import authService, { prepareSignUpData } from "../../Services/AuthService";

// Types
type Step = 1 | 2 | 3 | 4;

interface FormData {
  // Step 1: Account
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  
  // Step 2: Personal
  age: string;
  nationality: string;
  gender: string;
  
  // Step 3: Address
  city: string;
  street: string;
  plotNo: string;
  flatNo: string;
  postalCode: string;
  phoneNumber: string;
  
  // Step 4: Role
  role: string;
  
  // Role-specific fields
  specialization?: string;
  licenseNo?: string;
  pharmacistLicenseNo?: string;
  certification?: string;
}

export default function MultiStepSignUpForm() {
  const navigate = useNavigate();
  const [currentStep, setCurrentStep] = useState<Step>(1);
  const [showPassword, setShowPassword] = useState(false);
  const [isChecked, setIsChecked] = useState(false);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string>("");
  
  const [formData, setFormData] = useState<FormData>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    age: "",
    nationality: "",
    gender: "",
    city: "",
    street: "",
    plotNo: "",
    flatNo: "",
    postalCode: "",
    phoneNumber: "",
    role: "",
  });

  const handleInputChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
    setError(""); // Clear error on input change
  };

  const validateStep = (step: Step): boolean => {
    switch (step) {
      case 1:
        if (!formData.firstName || !formData.lastName || !formData.email || !formData.password) {
          setError("Please fill in all required fields");
          return false;
        }
        if (formData.password.length < 8) {
          setError("Password must be at least 8 characters");
          return false;
        }
        if (!isChecked) {
          setError("Please agree to the Terms and Conditions");
          return false;
        }
        return true;

      case 2:
        if (!formData.age || !formData.nationality || !formData.gender) {
          setError("Please fill in all required fields");
          return false;
        }
        const age = parseInt(formData.age);
        if (age < 10 || age > 150) {
          setError("Age must be between 10 and 150");
          return false;
        }
        return true;

      case 3:
        if (!formData.city || !formData.street || !formData.plotNo || !formData.postalCode) {
          setError("Please fill in all required fields");
          return false;
        }
        return true;

      case 4:
        if (!formData.role) {
          setError("Please select your role");
          return false;
        }
        
        // Validate role-specific fields
        if (formData.role === "doctor" || formData.role === "nurse") {
          if (!formData.specialization || !formData.licenseNo) {
            setError("Please provide your specialization and license number");
            return false;
          }
        } else if (formData.role === "pharmacist") {
          if (!formData.pharmacistLicenseNo) {
            setError("Please provide your pharmacy license number");
            return false;
          }
        } else if (formData.role === "lab_technician") {
          if (!formData.certification) {
            setError("Please provide your certification");
            return false;
          }
        }
        return true;

      default:
        return true;
    }
  };

  const nextStep = () => {
    if (!validateStep(currentStep)) {
      return;
    }

    if (currentStep < 4) {
      setCurrentStep((prev) => (prev + 1) as Step);
      window.scrollTo({ top: 0, behavior: "smooth" });
    }
  };

  const prevStep = () => {
    if (currentStep > 1) {
      setCurrentStep((prev) => (prev - 1) as Step);
      setError(""); // Clear error when going back
      window.scrollTo({ top: 0, behavior: "smooth" });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!validateStep(4)) {
      return;
    }

    setIsSubmitting(true);
    setError("");

    try {
      // Prepare data for API
      const signUpData = prepareSignUpData(formData);
      
      // Call the API
      const response = await authService.signUp(signUpData);

      if (response.success) {
        // Show success message
        console.log("Registration successful!", response);
        
        // Redirect to sign in or dashboard
        navigate("/signin", { 
          state: { 
            message: "Registration successful! Please sign in to continue." 
          } 
        });
      } else {
        setError(response.message || "Registration failed. Please try again.");
      }
    } catch (error) {
      console.error("Registration error:", error);
      setError(
        error instanceof Error 
          ? error.message 
          : "An unexpected error occurred. Please try again."
      );
    } finally {
      setIsSubmitting(false);
    }
  };

  const requiresProfessionalInfo = ["doctor", "nurse", "pharmacist", "lab_technician"].includes(formData.role);

  return (
    <div className="flex flex-col flex-1 w-full overflow-y-auto lg:w-1/2 no-scrollbar">
      <div className="w-full max-w-md mx-auto mb-5 sm:pt-10">
        <Link
          to="/"
          className="inline-flex items-center text-sm text-gray-500 transition-colors hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-300"
        >
          <ChevronLeftIcon className="size-5" />
          Back to dashboard
        </Link>
      </div>

      <div className="flex flex-col justify-center flex-1 w-full max-w-md mx-auto">
        <div>
          <div className="mb-5 sm:mb-8">
            <h1 className="mb-2 font-semibold text-gray-800 text-title-sm dark:text-white/90 sm:text-title-md">
              Sign Up
            </h1>
            <p className="text-sm text-gray-500 dark:text-gray-400">
              Create your account in a few simple steps
            </p>
          </div>

          {/* Error Message */}
          {error && (
            <div className="p-4 mb-5 border border-red-200 rounded-lg bg-red-50 dark:bg-red-900/10 dark:border-red-900/30">
              <div className="flex gap-3">
                <svg className="w-5 h-5 text-red-600 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd"/>
                </svg>
                <p className="text-sm text-red-800 dark:text-red-300">{error}</p>
              </div>
            </div>
          )}

          {/* Progress Stepper */}
          <div className="relative flex justify-between mb-8">
            <div className="absolute top-4 left-0 right-0 h-0.5 bg-gray-200 dark:bg-gray-800 -z-10" />

            {[1, 2, 3, 4].map((step) => (
              <div key={step} className="flex flex-col items-center flex-1">
                <div
                  className={`flex items-center justify-center w-8 h-8 rounded-full text-sm font-semibold transition-all ${
                    step < currentStep
                      ? "bg-green-500 text-white"
                      : step === currentStep
                      ? "bg-brand-500 text-white"
                      : "bg-white dark:bg-gray-900 border-2 border-gray-200 dark:border-gray-800 text-gray-400"
                  }`}
                >
                  {step < currentStep ? "✓" : step}
                </div>
                <span
                  className={`mt-2 text-xs font-medium ${
                    step === currentStep
                      ? "text-brand-500"
                      : step < currentStep
                      ? "text-green-500"
                      : "text-gray-400"
                  }`}
                >
                  {step === 1
                    ? "Account"
                    : step === 2
                    ? "Personal"
                    : step === 3
                    ? "Address"
                    : "Complete"}
                </span>
              </div>
            ))}
          </div>

          <form onSubmit={handleSubmit}>
            {/* Step 1: Account Information */}
            {currentStep === 1 && (
              <div className="space-y-5">
                <div className="grid grid-cols-1 gap-3 sm:grid-cols-2 sm:gap-5">
                  <button
                    type="button"
                    className="inline-flex items-center justify-center gap-3 py-3 text-sm font-normal text-gray-700 transition-colors bg-gray-100 rounded-lg px-7 hover:bg-gray-200 hover:text-gray-800 dark:bg-white/5 dark:text-white/90 dark:hover:bg-white/10"
                  >
                    <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
                      <path d="M18.7511 10.1944C18.7511 9.47495 18.6915 8.94995 18.5626 8.40552H10.1797V11.6527H15.1003C15.0011 12.4597 14.4654 13.675 13.2749 14.4916L13.2582 14.6003L15.9087 16.6126L16.0924 16.6305C17.7788 15.1041 18.7511 12.8583 18.7511 10.1944Z" fill="#4285F4"/>
                      <path d="M10.1788 18.75C12.5895 18.75 14.6133 17.9722 16.0915 16.6305L13.274 14.4916C12.5201 15.0068 11.5081 15.3666 10.1788 15.3666C7.81773 15.3666 5.81379 13.8402 5.09944 11.7305L4.99473 11.7392L2.23868 13.8295L2.20264 13.9277C3.67087 16.786 6.68674 18.75 10.1788 18.75Z" fill="#34A853"/>
                      <path d="M5.10014 11.7305C4.91165 11.186 4.80257 10.6027 4.80257 9.99992C4.80257 9.3971 4.91165 8.81379 5.09022 8.26935L5.08523 8.1534L2.29464 6.02954L2.20333 6.0721C1.5982 7.25823 1.25098 8.5902 1.25098 9.99992C1.25098 11.4096 1.5982 12.7415 2.20333 13.9277L5.10014 11.7305Z" fill="#FBBC05"/>
                      <path d="M10.1789 4.63331C11.8554 4.63331 12.9864 5.34303 13.6312 5.93612L16.1511 3.525C14.6035 2.11528 12.5895 1.25 10.1789 1.25C6.68676 1.25 3.67088 3.21387 2.20264 6.07218L5.08953 8.26943C5.81381 6.15972 7.81776 4.63331 10.1789 4.63331Z" fill="#EB4335"/>
                    </svg>
                    Sign up with Google
                  </button>
                  <button
                    type="button"
                    className="inline-flex items-center justify-center gap-3 py-3 text-sm font-normal text-gray-700 transition-colors bg-gray-100 rounded-lg px-7 hover:bg-gray-200 hover:text-gray-800 dark:bg-white/5 dark:text-white/90 dark:hover:bg-white/10"
                  >
                    <svg width="21" className="fill-current" height="20" viewBox="0 0 21 20" fill="none">
                      <path d="M15.6705 1.875H18.4272L12.4047 8.75833L19.4897 18.125H13.9422L9.59717 12.4442L4.62554 18.125H1.86721L8.30887 10.7625L1.51221 1.875H7.20054L11.128 7.0675L15.6705 1.875ZM14.703 16.475H16.2305L6.37054 3.43833H4.73137L14.703 16.475Z"/>
                    </svg>
                    Sign up with X
                  </button>
                </div>

                <div className="relative py-3 sm:py-5">
                  <div className="absolute inset-0 flex items-center">
                    <div className="w-full border-t border-gray-200 dark:border-gray-800"></div>
                  </div>
                  <div className="relative flex justify-center text-sm">
                    <span className="p-2 text-gray-400 bg-white dark:bg-gray-900 sm:px-5 sm:py-2">Or</span>
                  </div>
                </div>

                <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                  <div className="sm:col-span-1">
                    <Label>First Name<span className="text-error-500">*</span></Label>
                    <Input
                      type="text"
                      name="firstName"
                      value={formData.firstName}
                      onChange={handleInputChange}
                      placeholder="Enter your first name"
                      required
                    />
                  </div>
                  <div className="sm:col-span-1">
                    <Label>Last Name<span className="text-error-500">*</span></Label>
                    <Input
                      type="text"
                      name="lastName"
                      value={formData.lastName}
                      onChange={handleInputChange}
                      placeholder="Enter your last name"
                      required
                    />
                  </div>
                </div>

                <div>
                  <Label>Email<span className="text-error-500">*</span></Label>
                  <Input
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleInputChange}
                    placeholder="Enter your email"
                    required
                  />
                </div>

                <div>
                  <Label>Password<span className="text-error-500">*</span></Label>
                  <div className="relative">
                    <Input
                      placeholder="Enter your password"
                      type={showPassword ? "text" : "password"}
                      name="password"
                      value={formData.password}
                      onChange={handleInputChange}
                      required
                    />
                    <span
                      onClick={() => setShowPassword(!showPassword)}
                      className="absolute z-30 -translate-y-1/2 cursor-pointer right-4 top-1/2"
                    >
                      {showPassword ? (
                        <EyeIcon className="fill-gray-500 dark:fill-gray-400 size-5" />
                      ) : (
                        <EyeCloseIcon className="fill-gray-500 dark:fill-gray-400 size-5" />
                      )}
                    </span>
                  </div>
                  <p className="mt-1 text-xs text-gray-400">Must be at least 8 characters</p>
                </div>

                <div className="flex items-center gap-3">
                  <Checkbox className="w-5 h-5" checked={isChecked} onChange={setIsChecked} />
                  <p className="inline-block text-sm font-normal text-gray-500 dark:text-gray-400">
                    By creating an account means you agree to the{" "}
                    <span className="text-gray-800 dark:text-white/90">Terms and Conditions,</span> and our{" "}
                    <span className="text-gray-800 dark:text-white">Privacy Policy</span>
                  </p>
                </div>

                <div>
                  <button
                    type="button"
                    onClick={nextStep}
                    className="flex items-center justify-center w-full px-4 py-3 text-sm font-medium text-white transition rounded-lg bg-brand-500 shadow-theme-xs hover:bg-brand-600"
                  >
                    Continue
                  </button>
                </div>

                <div>
                  <p className="text-sm font-normal text-center text-gray-700 dark:text-gray-400 sm:text-start">
                    Already have an account?{" "}
                    <Link to="/signin" className="text-brand-500 hover:text-brand-600 dark:text-brand-400">
                      Sign In
                    </Link>
                  </p>
                </div>
              </div>
            )}

            {/* Step 2: Personal Information */}
            {currentStep === 2 && (
              <div className="space-y-5">
                <h2 className="text-lg font-semibold text-gray-800 dark:text-white/90">
                  Personal Information
                </h2>

                <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                  <div className="sm:col-span-1">
                    <Label>Age<span className="text-error-500">*</span></Label>
                    <Input
                      type="number"
                      name="age"
                      value={formData.age}
                      onChange={handleInputChange}
                      placeholder="Enter your age"
                      min="10"
                      max="150"
                      required
                    />
                    <p className="mt-1 text-xs text-gray-400">Must be between 10 and 150</p>
                  </div>
                  <div className="sm:col-span-1">
                    <Label>Nationality<span className="text-error-500">*</span></Label>
                    <select
                      name="nationality"
                      value={formData.nationality}
                      onChange={handleInputChange}
                      required
                      className="w-full px-4 py-3 text-sm text-gray-700 transition-colors bg-white border border-gray-200 rounded-lg outline-none dark:bg-gray-900 dark:border-gray-800 dark:text-white/90 focus:border-brand-500 dark:focus:border-brand-400"
                    >
                      <option value="">Select nationality</option>
                      <option value="PK">Pakistan</option>
                      <option value="US">United States</option>
                      <option value="UK">United Kingdom</option>
                      <option value="CA">Canada</option>
                      <option value="AU">Australia</option>
                      <option value="IN">India</option>
                      <option value="BD">Bangladesh</option>
                      <option value="AE">UAE</option>
                      <option value="SA">Saudi Arabia</option>
                    </select>
                  </div>
                </div>

                <div>
                  <Label>Gender<span className="text-error-500">*</span></Label>
                  <div className="flex gap-4 mt-2">
                    {["male", "female", "other"].map((gender) => (
                      <label key={gender} className="flex items-center gap-2 cursor-pointer">
                        <input
                          type="radio"
                          name="gender"
                          value={gender}
                          checked={formData.gender === gender}
                          onChange={handleInputChange}
                          required
                          className="w-4 h-4 text-brand-500 border-gray-300 cursor-pointer focus:ring-brand-500"
                        />
                        <span className="text-sm text-gray-700 capitalize dark:text-gray-300">{gender}</span>
                      </label>
                    ))}
                  </div>
                </div>

                <div className="flex gap-3">
                  <button
                    type="button"
                    onClick={prevStep}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-gray-700 transition bg-gray-100 rounded-lg hover:bg-gray-200 dark:bg-white/5 dark:text-white/90 dark:hover:bg-white/10"
                  >
                    Back
                  </button>
                  <button
                    type="button"
                    onClick={nextStep}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-white transition rounded-lg bg-brand-500 shadow-theme-xs hover:bg-brand-600"
                  >
                    Continue
                  </button>
                </div>
              </div>
            )}

            {/* Step 3: Address Information */}
            {currentStep === 3 && (
              <div className="space-y-5">
                <h2 className="text-lg font-semibold text-gray-800 dark:text-white/90">
                  Address Information
                </h2>

                <div>
                  <Label>City<span className="text-error-500">*</span></Label>
                  <Input
                    type="text"
                    name="city"
                    value={formData.city}
                    onChange={handleInputChange}
                    placeholder="Enter your city"
                    maxLength={32}
                    required
                  />
                </div>

                <div>
                  <Label>Street<span className="text-error-500">*</span></Label>
                  <Input
                    type="text"
                    name="street"
                    value={formData.street}
                    onChange={handleInputChange}
                    placeholder="Enter street name"
                    maxLength={32}
                    required
                  />
                </div>

                <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                  <div className="sm:col-span-1">
                    <Label>Plot No<span className="text-error-500">*</span></Label>
                    <Input
                      type="text"
                      name="plotNo"
                      value={formData.plotNo}
                      onChange={handleInputChange}
                      placeholder="Plot number"
                      maxLength={8}
                      required
                    />
                  </div>
                  <div className="sm:col-span-1">
                    <Label>Flat No</Label>
                    <Input
                      type="text"
                      name="flatNo"
                      value={formData.flatNo}
                      onChange={handleInputChange}
                      placeholder="Flat number (optional)"
                      maxLength={8}
                    />
                  </div>
                </div>

                <div>
                  <Label>Postal Code<span className="text-error-500">*</span></Label>
                  <Input
                    type="text"
                    name="postalCode"
                    value={formData.postalCode}
                    onChange={handleInputChange}
                    placeholder="Enter postal code"
                    maxLength={8}
                    required
                  />
                </div>

                <div>
                  <Label>Phone Number</Label>
                  <Input
                    type="tel"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleInputChange}
                    placeholder="Enter phone number (optional)"
                  />
                </div>

                <div className="flex gap-3">
                  <button
                    type="button"
                    onClick={prevStep}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-gray-700 transition bg-gray-100 rounded-lg hover:bg-gray-200 dark:bg-white/5 dark:text-white/90 dark:hover:bg-white/10"
                  >
                    Back
                  </button>
                  <button
                    type="button"
                    onClick={nextStep}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-white transition rounded-lg bg-brand-500 shadow-theme-xs hover:bg-brand-600"
                  >
                    Continue
                  </button>
                </div>
              </div>
            )}

            {/* Step 4: Role Selection & Professional Info */}
            {currentStep === 4 && (
              <div className="space-y-5">
                <h2 className="text-lg font-semibold text-gray-800 dark:text-white/90">
                  Almost Done!
                </h2>

                <div>
                  <Label>I am a<span className="text-error-500">*</span></Label>
                  <select
                    name="role"
                    value={formData.role}
                    onChange={handleInputChange}
                    required
                    className="w-full px-4 py-3 text-sm text-gray-700 transition-colors bg-white border border-gray-200 rounded-lg outline-none dark:bg-gray-900 dark:border-gray-800 dark:text-white/90 focus:border-brand-500 dark:focus:border-brand-400"
                  >
                    <option value="">Select your role</option>
                    <option value="patient">Patient</option>
                    <option value="doctor">Doctor</option>
                    <option value="nurse">Nurse</option>
                    <option value="pharmacist">Pharmacist</option>
                    <option value="lab_technician">Lab Technician</option>
                  </select>
                </div>

                {/* Dynamic Role-Specific Fields */}
                {(formData.role === "doctor" || formData.role === "nurse") && (
                  <div className="p-5 space-y-5 border border-gray-200 rounded-lg bg-gray-50 dark:bg-white/5 dark:border-gray-800">
                    <div className="flex items-start gap-2">
                      <svg className="w-5 h-5 text-brand-500 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clipRule="evenodd"/>
                      </svg>
                      <div>
                        <h3 className="text-sm font-medium text-gray-800 dark:text-white">
                          Professional Credentials Required
                        </h3>
                        <p className="mt-1 text-xs text-gray-500 dark:text-gray-400">
                          Please provide your medical credentials for verification
                        </p>
                      </div>
                    </div>

                    <div>
                      <Label>Specialization<span className="text-error-500">*</span></Label>
                      <Input
                        type="text"
                        name="specialization"
                        value={formData.specialization || ""}
                        onChange={handleInputChange}
                        placeholder="e.g., Cardiology, Pediatrics, Emergency Care"
                        required
                      />
                    </div>

                    <div>
                      <Label>Medical License Number<span className="text-error-500">*</span></Label>
                      <Input
                        type="text"
                        name="licenseNo"
                        value={formData.licenseNo || ""}
                        onChange={handleInputChange}
                        placeholder="Enter your license number"
                        required
                      />
                    </div>
                  </div>
                )}

                {formData.role === "pharmacist" && (
                  <div className="p-5 space-y-5 border border-gray-200 rounded-lg bg-gray-50 dark:bg-white/5 dark:border-gray-800">
                    <div className="flex items-start gap-2">
                      <svg className="w-5 h-5 text-brand-500 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clipRule="evenodd"/>
                      </svg>
                      <div>
                        <h3 className="text-sm font-medium text-gray-800 dark:text-white">
                          Professional Credentials Required
                        </h3>
                        <p className="mt-1 text-xs text-gray-500 dark:text-gray-400">
                          Please provide your pharmacy license information
                        </p>
                      </div>
                    </div>

                    <div>
                      <Label>Pharmacy License Number<span className="text-error-500">*</span></Label>
                      <Input
                        type="text"
                        name="pharmacistLicenseNo"
                        value={formData.pharmacistLicenseNo || ""}
                        onChange={handleInputChange}
                        placeholder="Enter your pharmacy license number"
                        required
                      />
                    </div>
                  </div>
                )}

                {formData.role === "lab_technician" && (
                  <div className="p-5 space-y-5 border border-gray-200 rounded-lg bg-gray-50 dark:bg-white/5 dark:border-gray-800">
                    <div className="flex items-start gap-2">
                      <svg className="w-5 h-5 text-brand-500 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a1 1 0 000 2v3a1 1 0 001 1h1a1 1 0 100-2v-3a1 1 0 00-1-1H9z" clipRule="evenodd"/>
                      </svg>
                      <div>
                        <h3 className="text-sm font-medium text-gray-800 dark:text-white">
                          Professional Credentials Required
                        </h3>
                        <p className="mt-1 text-xs text-gray-500 dark:text-gray-400">
                          Please provide your certification details
                        </p>
                      </div>
                    </div>

                    <div>
                      <Label>Certification<span className="text-error-500">*</span></Label>
                      <Input
                        type="text"
                        name="certification"
                        value={formData.certification || ""}
                        onChange={handleInputChange}
                        placeholder="e.g., MLT, MT, CLS"
                        required
                      />
                    </div>
                  </div>
                )}

                {requiresProfessionalInfo && (
                  <div className="p-4 border border-yellow-200 rounded-lg bg-yellow-50 dark:bg-yellow-900/10 dark:border-yellow-900/30">
                    <div className="flex gap-3">
                      <svg className="w-5 h-5 text-yellow-600 flex-shrink-0 mt-0.5" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M8.257 3.099c.765-1.36 2.722-1.36 3.486 0l5.58 9.92c.75 1.334-.213 2.98-1.742 2.98H4.42c-1.53 0-2.493-1.646-1.743-2.98l5.58-9.92zM11 13a1 1 0 11-2 0 1 1 0 012 0zm-1-8a1 1 0 00-1 1v3a1 1 0 002 0V6a1 1 0 00-1-1z" clipRule="evenodd"/>
                      </svg>
                      <div>
                        <h4 className="text-sm font-medium text-yellow-800 dark:text-yellow-300">
                          Verification Pending
                        </h4>
                        <p className="mt-1 text-xs text-yellow-700 dark:text-yellow-400">
                          Your credentials will be verified by our admin team. You'll have limited access until verification is complete.
                        </p>
                      </div>
                    </div>
                  </div>
                )}

                <div className="flex gap-3 pt-2">
                  <button
                    type="button"
                    onClick={prevStep}
                    disabled={isSubmitting}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-gray-700 transition bg-gray-100 rounded-lg hover:bg-gray-200 dark:bg-white/5 dark:text-white/90 dark:hover:bg-white/10 disabled:opacity-50"
                  >
                    Back
                  </button>
                  <button
                    type="submit"
                    disabled={isSubmitting}
                    className="flex items-center justify-center flex-1 px-4 py-3 text-sm font-medium text-white transition rounded-lg bg-brand-500 shadow-theme-xs hover:bg-brand-600 disabled:opacity-50"
                  >
                    {isSubmitting ? (
                      <>
                        <svg className="w-5 h-5 mr-2 animate-spin" fill="none" viewBox="0 0 24 24">
                          <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"/>
                          <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"/>
                        </svg>
                        Submitting...
                      </>
                    ) : (
                      "Complete Registration"
                    )}
                  </button>
                </div>
              </div>
            )}
          </form>
        </div>
      </div>
    </div>
  );
}
