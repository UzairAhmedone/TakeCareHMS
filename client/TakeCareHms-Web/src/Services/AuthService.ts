// services/AuthService.ts

// ============================================
// TYPES & INTERFACES
// ============================================

export enum Gender {
  Male = 1,
  Female = 2,
  Other = 3,
}

export enum UserRole {
  Patient = 1,
  Doctor = 2,
  Nurse = 3,
  Pharmacist = 4,
  LabTechnician = 5,
}

export interface DoctorProfileRequest {
  specialization: string;
  licenseNo: string;
}

export interface NurseProfileRequest {
  specialization: string;
  licenseNo: string;
}

export interface PharmacistProfileRequest {
  licenseNo: string;
}

export interface LabTechnicianProfileRequest {
  certification: string;
}

export interface SignUpRequest {
  firstName: string;
  lastName: string;
  fullName: string | null;
  password: string;
  email: string;
  userName: string;
  age: number;
  nationality: string;
  city: string;
  street: string;
  plotNo: string;
  flatNo: string | null;
  postalCode: string;
  phoneNumber: string;
  gender: Gender;
  role: UserRole;
  doctorProfile?: DoctorProfileRequest | null;
  nurseProfile?: NurseProfileRequest | null;
  pharmacistProfile?: PharmacistProfileRequest | null;
  labTechnicianProfile?: LabTechnicianProfileRequest | null;
}

export interface SignUpResponse {
  success: boolean;
  message: string;
  userId?: string;
  token?: string;
}

export interface SignInRequest {
  email: string;
  password: string;
}

export interface SignInResponse {
  success: boolean;
  message: string;
  token: string;
  user: {
    id: string;
    email: string;
    fullName: string;
    role: UserRole;
  };
}

// ============================================
// API CONFIGURATION
// ============================================

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;;

// ============================================
// AUTH SERVICE CLASS
// ============================================

class AuthService {
  private baseUrl: string;

  constructor(baseUrl: string = API_BASE_URL) {
    this.baseUrl = baseUrl;
  }

  /**
   * Sign up a new user
   */
  async signUp(data: SignUpRequest): Promise<SignUpResponse> {
    try {
      const response = await fetch(`${this.baseUrl}/Auth/Signup`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Sign up failed');
      }

      const result = await response.json();
      return {
        success: true,
        message: 'Registration successful',
        ...result,
      };
    } catch (error) {
      console.error('Sign up error:', error);
      return {
        success: false,
        message: error instanceof Error ? error.message : 'An error occurred during sign up',
      };
    }
  }

  /**
   * Sign in an existing user
   */
  async signIn(data: SignInRequest): Promise<SignInResponse> {
    try {
      const response = await fetch(`${this.baseUrl}/Auth/Signin`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Sign in failed');
      }

      const result = await response.json();
      
      // Store token in localStorage
      if (result.token) {
        localStorage.setItem('authToken', result.token);
        localStorage.setItem('user', JSON.stringify(result.user));
      }

      return {
        success: true,
        ...result,
      };
    } catch (error) {
      console.error('Sign in error:', error);
      throw error;
    }
  }

  /**
   * Sign out the current user
   */
  signOut(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('user');
  }

  /**
   * Get the current authentication token
   */
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  /**
   * Get the current user
   */
  getCurrentUser(): any | null {
    const userStr = localStorage.getItem('user');
    return userStr ? JSON.parse(userStr) : null;
  }

  /**
   * Check if user is authenticated
   */
  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}

// ============================================
// HELPER FUNCTIONS
// ============================================

/**
 * Convert string role to enum
 */
export function getRoleEnum(role: string): UserRole {
  const roleMap: Record<string, UserRole> = {
    patient: UserRole.Patient,
    doctor: UserRole.Doctor,
    nurse: UserRole.Nurse,
    pharmacist: UserRole.Pharmacist,
    lab_technician: UserRole.LabTechnician,
  };
  return roleMap[role] || UserRole.Patient;
}

/**
 * Convert string gender to enum
 */
export function getGenderEnum(gender: string): Gender {
  const genderMap: Record<string, Gender> = {
    male: Gender.Male,
    female: Gender.Female,
    other: Gender.Other,
  };
  return genderMap[gender] || Gender.Other;
}

/**
 * Prepare sign up data from form data
 */
export function prepareSignUpData(formData: any): SignUpRequest {
  const fullName = `${formData.firstName} ${formData.lastName}`;
  const userName = formData.email.split('@')[0]; // Generate username from email
  const role = getRoleEnum(formData.role);
  const gender = getGenderEnum(formData.gender);

  const signUpData: SignUpRequest = {
    firstName: formData.firstName,
    lastName: formData.lastName,
    fullName: fullName,
    password: formData.password,
    email: formData.email,
    userName: userName,
    age: parseInt(formData.age),
    nationality: formData.nationality,
    city: formData.city,
    street: formData.street,
    plotNo: formData.plotNo,
    flatNo: formData.flatNo || null,
    postalCode: formData.postalCode,
    phoneNumber: formData.phoneNumber || '',
    gender: gender,
    role: role,
  };

  // Add role-specific profiles
  switch (formData.role) {
    case 'doctor':
      signUpData.doctorProfile = {
        specialization: formData.specialization || '',
        licenseNo: formData.licenseNo || '',
      };
      signUpData.nurseProfile = null;
      signUpData.pharmacistProfile = null;
      signUpData.labTechnicianProfile = null;
      break;

    case 'nurse':
      signUpData.nurseProfile = {
        specialization: formData.specialization || '',
        licenseNo: formData.licenseNo || '',
      };
      signUpData.doctorProfile = null;
      signUpData.pharmacistProfile = null;
      signUpData.labTechnicianProfile = null;
      break;

    case 'pharmacist':
      signUpData.pharmacistProfile = {
        licenseNo: formData.pharmacistLicenseNo || '',
      };
      signUpData.doctorProfile = null;
      signUpData.nurseProfile = null;
      signUpData.labTechnicianProfile = null;
      break;

    case 'lab_technician':
      signUpData.labTechnicianProfile = {
        certification: formData.certification || '',
      };
      signUpData.doctorProfile = null;
      signUpData.nurseProfile = null;
      signUpData.pharmacistProfile = null;
      break;

    case 'patient':
    default:
      signUpData.doctorProfile = null;
      signUpData.nurseProfile = null;
      signUpData.pharmacistProfile = null;
      signUpData.labTechnicianProfile = null;
      break;
  }

  return signUpData;
}

// ============================================
// EXPORT SINGLETON INSTANCE
// ============================================

const authService = new AuthService();
export default authService;