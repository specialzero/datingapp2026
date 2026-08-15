export interface User {
    id: string;
    displayName: string;
    email: string;
    imageUrl: string | null;
    token: string;
}

export interface LoginCreds {
    email: string;
    password: string;
}

export interface RegisterCreds {
    email: string;
    displayName: string;
    password: string;
}