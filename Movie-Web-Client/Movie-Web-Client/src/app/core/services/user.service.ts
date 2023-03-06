import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiUrlConstants } from "../common/api-url.constants";

@Injectable({
    providedIn: 'root'
})
export class UserService {
    constructor(private http: HttpClient) {
    }

    getUserProfile(id:string) :Observable<any[]>
    {
        return this.http.get<any[]>(ApiUrlConstants.API_URL + 'users/' + id);
    }
}