import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    // Check if the user is logged in
    if (!this.authService.isLoggedIn()) {
      // If not logged in, redirect to the login page
      this.router.navigate(['/login']);
      return false;
    }

    // Check if the user has the 'isAdmin' or 'isOrganizer' role
    if (route.url[0].path === 'applicationform' && !this.authService.isAdmin()) {
      // If not an admin, redirect to a forbidden page or show an error
      this.router.navigate(['/forbidden']);
      return false;
    }
    console.log("path  "+route.url[0].path);

    if (route.url[0].path === 'admin' && !this.authService.isAdmin()) {
      // If not an admin, redirect to a forbidden page or show an error
      this.router.navigate(['/forbidden']);
      return false;
    }


    if (route.url[0].path === 'student' && !this.authService.isStudent()) {
      // If not an organizer, redirect to a forbidden page or show an error
      this.router.navigate(['/forbidden']);
      return false;
    }

    // If the user is logged in and has the appropriate role, allow access
    return true;
  }

}
