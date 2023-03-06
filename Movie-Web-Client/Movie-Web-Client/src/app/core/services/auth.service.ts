import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { ApiUrlConstants } from "../common/api-url.constants";

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
}