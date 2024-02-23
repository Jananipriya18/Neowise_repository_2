import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private authService: AuthService, private router: Router) {}

//   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
//     // Check if the user is logged in
//     if (!this.authService.isLoggedIn()) {
//       // If not logged in and trying to access login or register, allow access
//       if (state.url === '/login' || state.url === '/register') {
//         return true;
//       }
  
//       // If not logged in and not trying to access login or register, redirect to login
//       this.router.navigate(['/login']);
//       return false;
//     }

//     // Check if the user has the 'isAdmin' or 'isOrganizer' role
//     if (route.url[0].path === 'applicationform' && !this.authService.isAdmin()) {
//       // If not an admin, redirect to a forbidden page or show an error
//       this.router.navigate(['/forbidden']);
//       return false;
//     }
//     console.log("path  "+route.url[0].path);

//     if (route.url[0].path === 'admin' && !this.authService.isAdmin()) {
//       // If not an admin, redirect to a forbidden page or show an error
//       this.router.navigate(['/forbidden']);
//       return false;
//     }


//     if (route.url[0].path === 'student' && !this.authService.isStudent()) {
//       // If not an organizer, redirect to a forbidden page or show an error
//       this.router.navigate(['/forbidden']);
//       return false;
//     }

//     // If the user is logged in and has the appropriate role, allow access
//     return true;
//   }

// }
canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
  // Check if the user is logged in
  if (!this.authService.isLoggedIn()) {
    // If not logged in and not trying to access a restricted page, allow access
    if (!this.isRestrictedPage(state.url)) {
      return true;
    }

    // If not logged in and trying to access a restricted page, redirect to login
    this.router.navigate(['/login']);
    return false;
  }

  // Check if the user has the 'isAdmin' or 'isOrganizer' role
  if (route.url[0].path === 'applicationform' && !this.authService.isAdmin()) {
    // If not an admin, redirect to a forbidden page or show an error
    this.router.navigate(['/forbidden']);
    return false;
  }

  console.log("path " + route.url[0].path);

  if (route.url[0].path === 'admin' && !this.authService.isAdmin()) {
    // If not an admin, redirect to a forbidden page or show an error
    this.router.navigate(['/forbidden']);
    return false;
  }

  // Allow access to all other routes if the user is logged in and has the appropriate role
  return true;
}

private isRestrictedPage(url: string): boolean {
  // Add logic to check if the provided URL is a restricted page
  // For example, check if it is neither 'login' nor 'signup'
  return url !== '/login' && url !== '/signup';
}
}