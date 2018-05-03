import { User } from "../models/user.model";

export class UserLocalstorage {
    public getUserLogged(): User {
        var userLoggedLocalStorage = JSON.parse(localStorage.getItem("userLoggedLocalStorage"));
        return userLoggedLocalStorage;
    }

    public setUserLogged(user: User): void {
        localStorage.setItem("userLoggedLocalStorage", JSON.stringify(user));
    }

    public removeUserLogged(): void {
        localStorage.removeItem("userLoggedLocalStorage");
    }
}