interface ValidationField {
    name: string;
    message: string;
  }
  
  export interface ApiResponse<T> {
    message: string;
    data: T;
    errors?: ValidationField[];
  }