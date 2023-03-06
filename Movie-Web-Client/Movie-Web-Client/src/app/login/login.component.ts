import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { result } from "lodash";
import { AuthenService } from "../core/services/auth.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    constructor(private router: Router, private authService: AuthenService) {

    }
    ngOnInit(): void {
    }

    loginSubmit()
    {
        this.authService.login("Khoi","123").subscribe(
            result => {
                console.log(result)
                //localStorage.setItem("access_token", result);
                this.router.navigateByUrl("/main", { skipLocationChange: true });
            }
        )
    }
}