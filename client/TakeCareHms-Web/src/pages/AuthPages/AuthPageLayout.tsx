import React from "react";
import GridShape from "../../components/common/GridShape";
import { Link } from "react-router";
import ThemeTogglerTwo from "../../components/common/ThemeTogglerTwo";

export default function AuthPageLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="relative p-6 bg-white z-1 dark:bg-gray-900 sm:p-0">
      <div className="relative flex flex-col justify-center w-full h-screen lg:flex-row dark:bg-gray-900 sm:p-0">
        {children}
        
        {/* Right Panel - HMS Branding */}
        <div className="hidden lg:flex items-center justify-center w-full lg:w-1/2 h-full bg-gradient-to-br from-blue-900 via-indigo-900 to-purple-900 relative overflow-hidden">
          {/* Grid Pattern Background */}
          <div className="absolute inset-0 opacity-30">
            <GridShape />
          </div>
          
          <div className="relative z-10 flex flex-col items-center max-w-md p-12 text-center">
            {/* Logo */}
            <div className="flex items-center justify-center w-20 h-20 mb-6 bg-brand-500/80 rounded-2xl backdrop-blur-sm shadow-2xl">
              <svg 
                className="w-10 h-10 text-white" 
                fill="none" 
                stroke="currentColor" 
                strokeWidth="2" 
                viewBox="0 0 24 24"
              >
                <path d="M12 2L2 7l10 5 10-5-10-5z"/>
                <path d="M2 17l10 5 10-5M2 12l10 5 10-5"/>
              </svg>
            </div>

            {/* Title */}
            <h1 className="mb-4 text-5xl font-bold text-white">HMS Admin</h1>
            <p className="mb-12 text-base text-white/80 leading-relaxed">
              Modern Healthcare Management System for the digital age. Streamline patient care, appointments, and medical records.
            </p>

            {/* Features */}
            <div className="space-y-6 text-left w-full">
              {/* Feature 1 - Secure & Compliant */}
              <div className="flex items-start gap-4">
                <div className="flex items-center justify-center flex-shrink-0 w-12 h-12 bg-white/10 rounded-xl backdrop-blur-sm">
                  <svg 
                    className="w-6 h-6 text-white" 
                    fill="none" 
                    stroke="currentColor" 
                    strokeWidth="2" 
                    viewBox="0 0 24 24"
                  >
                    <path d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"/>
                  </svg>
                </div>
                <div>
                  <h3 className="mb-1 text-base font-semibold text-white">
                    Secure & Compliant
                  </h3>
                  <p className="text-sm text-white/70">
                    HIPAA compliant with end-to-end encryption for all medical data
                  </p>
                </div>
              </div>

              {/* Feature 2 - Lightning Fast */}
              <div className="flex items-start gap-4">
                <div className="flex items-center justify-center flex-shrink-0 w-12 h-12 bg-white/10 rounded-xl backdrop-blur-sm">
                  <svg 
                    className="w-6 h-6 text-white" 
                    fill="none" 
                    stroke="currentColor" 
                    strokeWidth="2" 
                    viewBox="0 0 24 24"
                  >
                    <path d="M13 10V3L4 14h7v7l9-11h-7z"/>
                  </svg>
                </div>
                <div>
                  <h3 className="mb-1 text-base font-semibold text-white">
                    Lightning Fast
                  </h3>
                  <p className="text-sm text-white/70">
                    Access patient records and medical history in milliseconds
                  </p>
                </div>
              </div>

              {/* Feature 3 - Team Collaboration */}
              <div className="flex items-start gap-4">
                <div className="flex items-center justify-center flex-shrink-0 w-12 h-12 bg-white/10 rounded-xl backdrop-blur-sm">
                  <svg 
                    className="w-6 h-6 text-white" 
                    fill="none" 
                    stroke="currentColor" 
                    strokeWidth="2" 
                    viewBox="0 0 24 24"
                  >
                    <path d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z"/>
                  </svg>
                </div>
                <div>
                  <h3 className="mb-1 text-base font-semibold text-white">
                    Team Collaboration
                  </h3>
                  <p className="text-sm text-white/70">
                    Connect doctors, nurses, and staff for better patient outcomes
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>

        {/* Theme Toggle Button */}
        <div className="fixed z-50 hidden bottom-6 right-6 sm:block">
          <ThemeTogglerTwo />
        </div>
      </div>
    </div>
  );
}
