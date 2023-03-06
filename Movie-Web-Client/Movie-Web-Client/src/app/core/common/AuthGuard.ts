import { Injectable } from "@angular/core"
import { CanActivate, Router } from "@angular/router"

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(private router: Router) { }

    canActivate(): boolean {
        this.router.navigateByUrl("/login", { skipLocationChange: true });
        return false;
    }
}