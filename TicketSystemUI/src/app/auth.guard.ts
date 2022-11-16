import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { userToken } from './models/userToken';
@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private jwtHelper: JwtHelperService, private http: HttpClient) { }

    async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const token = localStorage.getItem("token");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }

        const isRefreshSuccess = await this.tryRefreshingTokens(token);
        if (!isRefreshSuccess) {
            this.router.navigate(["login"]);
        }
        return isRefreshSuccess;
    }

    private async tryRefreshingTokens(token: string | null): Promise<boolean> {
        const refreshToken = localStorage.getItem("refreshToken");
        if (!token || !refreshToken) {
            return false;
        }

        const credentials = JSON.stringify({ token: token, refreshToken: refreshToken });
        let isRefreshSuccess: boolean;
        const refreshRes = await new Promise<userToken>((resolve, reject) => {
            this.http.post<userToken>("https://localhost:44322/api/tokens/refresh", credentials, {
                headers: new HttpHeaders({
                    "Content-Type": "application/json"
                })
            }).subscribe({
                next: (res: userToken) => resolve(res),
                error: (_: any) => { reject; isRefreshSuccess = false; }
            });
        });
        localStorage.setItem("token", refreshRes.token);
        localStorage.setItem("refreshToken", refreshRes.refreshToken);
        isRefreshSuccess = true;
        return isRefreshSuccess;
    }


}
