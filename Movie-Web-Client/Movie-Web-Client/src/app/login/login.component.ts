import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthenRequest } from "../core/models/authenRequest.model";
import { userDTO } from "../core/models/register.model";
import { AuthenService } from "../core/services/auth.service";
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    public authRequest : AuthenRequest
    public userDTO: userDTO
    public confirmPassword:string=''
    public otpCode:string=''
    public FormStatus: Boolean = false
    public OTPMessage:string=''
    public PasswordMessage:string=''
    public clicked:boolean = true
    constructor(private router: Router, private authService: AuthenService, private activatedRoute: ActivatedRoute, private http: HttpClient) {
        this.authRequest = new AuthenRequest();
        this.userDTO = new userDTO();
    }
    ngOnInit(): void {
        this.PasswordMessage = "Password and confirm password is not match !"
        this.checkLoginGoogle();
    }

    checkLoginGoogle() {
        this.activatedRoute.queryParams.subscribe(params => {
            const token = params['token'];
            const userId = params['userId'];

            console.log(token, '---', userId)

            if (token && userId) {
                localStorage.setItem("access_token", token);
                localStorage.setItem("user_id", userId);
                this.router.navigateByUrl("/main", { skipLocationChange: true });
            }
        });
    }

    loginSubmit()
    {
      this.authService.login(this.authRequest.Email, this.authRequest.Password).subscribe(
          (result: any) => {
              if(result.data != null)
              {
                  localStorage.setItem("access_token", result.data.jwt);
                  localStorage.setItem("user_id", result.data.id);
                  this.router.navigateByUrl("/main", { skipLocationChange: true });
              }
              else
              {
                  alert(result.errorMessage)
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

    async sendOTP()
    {
        if(this.userDTO.Email != '')
        {
            this.clicked = false;
            await this.authService.sendOTP(this.userDTO.Email).subscribe(()=>{
                setTimeout(()=>{
                    this.authService.clearOTP(this.userDTO.Email).subscribe(() =>
                    {
                        this.clicked = true;
                    })
                }, 30000);
            })
        }
    }

    registerSubmit()
    {
        if(this.checkProperties(this.userDTO))
        {
            if(this.userDTO.Password == this.confirmPassword)
            {
              if(this.otpCode != '')
              {
                this.authService.verifyOTP(this.otpCode, this.userDTO.Email).subscribe((result:any)=>{
                  if(result.errorMessage == '' || result.errorMessage == null)
                  {
                      this.authService.register(this.userDTO.Username, this.userDTO.Email, this.userDTO.Password, this.userDTO.FirstName, this.userDTO.LastName).subscribe((result:any) =>
                      {
                          if(result.data != null)
                          {
                              this.authService.login(this.userDTO.Email, this.userDTO.Password).subscribe(
                                  (result: any) => {
                                      localStorage.setItem("access_token", result.jwt);
                                      localStorage.setItem("user_id", result.id);
                                      this.router.navigateByUrl("/main", { skipLocationChange: true });
                                  }
                              )
                          }
                          else
                          {
                              alert(result.errorMessage)
                          }
                      })
                  }
                  else
                  {
                      this.OTPMessage = "OTP is not match !";
                  }
                })
              }
              else
                alert("Please fill otp code")
            }
        }
        else
            alert("Please fill full information !")
    }
    checkProperties(obj:any) {
        for (var key in obj) {
            if (obj[key] == null || obj[key] == "")
                return false;
        }
        return true;
    }
}
