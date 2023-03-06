import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenRequest } from "../core/models/authenRequest.model";
import { AuthenService } from "../core/services/auth.service";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    public authRequest : AuthenRequest
    public FormStatus: Boolean = false
    constructor(private router: Router, private authService: AuthenService) {
        this.authRequest = new AuthenRequest();
    }
    ngOnInit(): void {
    }

    loginSubmit()
    {
        this.authService.login(this.authRequest.Username, this.authRequest.Password).subscribe(
            (result: any) => {
                if(result.jwtToken != null)
                {
                    localStorage.setItem("access_token", result.jwtToken);
                    localStorage.setItem("user_id", result.id);
                    this.router.navigateByUrl("/main", { skipLocationChange: true });
                }
                else
                {
                    alert("Username or password incorrect !")
                }
            }
        )
    }
    redirectLoginForm()
    {
        this.FormStatus = false;
    }

    redirectRegisterForm()
    {
        this.FormStatus = true;
    }
}