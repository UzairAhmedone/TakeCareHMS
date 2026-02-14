import PageMeta from "../../components/common/PageMeta";
import AuthLayout from "./AuthPageLayout";
import SignUpForm from "../../components/auth/SignUpForm";


export default function SignUp() {
  return (
    <>
      <PageMeta
        title="Sign Up HMS"
        description="Sign up to access the HMS dashboard and manage your hospital efficiently."
      />
      <AuthLayout>
        <SignUpForm />
      </AuthLayout>
    </>
  );
}
