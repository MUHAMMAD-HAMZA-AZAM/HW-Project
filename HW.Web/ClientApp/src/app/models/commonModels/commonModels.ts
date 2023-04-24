export class LoginVM {
  emailOrPhoneNumber: string;
  password: string;
}

export class ResponseVm
{
  status: any;
  message: string;
  resultData: any;
}

export class forgotPasswrodVm {
  id: string;
  email: string;
  password: string;
  confirmPassword: string;
  phoneNumber: string;
  role: string;
  firebaseClientId: string;
  verificationCode: string;
  clientId: string;
}
export class ResetPassword {
  userId: string;
  passwordResetToken: string;
  password: string;
  confirmPassword: string;
}
export class ResetPasswordVm {
  password: string;
  confirmPassword: string;
}

export class BasicRegistration {
  role: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  cnic: number;
  emailAddress: string;
  password: string;
  gender: number;
  phoneNumber: number;
  city: string;
  termsAndcondition: boolean;
  verificationCode: string;
  emailOrPhoneNumber: string;
}
export class IdValueVm {
  id: number;
  value: string;
}
