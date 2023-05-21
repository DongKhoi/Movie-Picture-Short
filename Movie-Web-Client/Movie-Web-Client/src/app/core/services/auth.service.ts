import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { ApiUrlConstants } from "../common/api-url.constants";
import { userDTO } from "../models/register.model";
@Injectable({
    providedIn: 'root'
})
export class AuthenService {
    constructor(private router: Router, private http: HttpClient) {
    }

    login(userName: string, passWord: string) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'identity/authenticate',{userName, passWord});
    }

    logout(userId : string) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'identity/revoke-token/' + userId,{});
    }

    register(userName:string, email:string, password:string, firstName:string, lastName:string) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'users/register',{userName, email, password, firstName, lastName});
    }

    sendOTP(mailAddress : string) : Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'otps/send-email-otp/'+ mailAddress, {});
    }
    verifyOTP(oTPCode:string, mailAddress : string) : Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'otps/verify-otp', {oTPCode,mailAddress});
    }
    clearOTP(mailAddress : string) : Observable<any[]>
    {
        return this.http.delete<any[]>(ApiUrlConstants.API_URL + 'otps/expired-otp/'+ mailAddress, {});
    }
}