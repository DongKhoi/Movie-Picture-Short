import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { ApiUrlConstants } from "../common/api-url.constants";
import { userDTO } from "../models/register.model";
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class AuthenService {
    constructor(private router: Router, private http: HttpClient) {
    }

    login(email: string, passWord: string): Observable<any[]> {
      const currentUrl = window.location.href;
      const headers = new HttpHeaders().set('KarrotUrl', currentUrl);

      return this.http.post<any[]>(ApiUrlConstants.API_URL + 'identity/provider-login', { email, passWord }, { headers });
    }

    logout(userId : string) :Observable<any[]>
    {
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'identity/revoke-token/' + userId,{});
    }

    register(userName:string, email:string, password:string, firstName:string, lastName:string) :Observable<any[]>
    {
      const currentUrl = window.location.href;
      const headers = new HttpHeaders().set('KarrotUrl', currentUrl);
        return this.http.post<any[]>(ApiUrlConstants.API_URL + 'identity/provider-register',{userName, email, password, firstName, lastName}, { headers });
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
