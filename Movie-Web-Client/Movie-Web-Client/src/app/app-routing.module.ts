import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/common/AuthGuard';

const routes: Routes = [
  { path: '', redirectTo: 'main', pathMatch: 'full' },

  { path: 'login', loadChildren:() => import('./login/login.module').then(x => x.LoginModule) },
  { path: 'main', loadChildren:() => import('./main/main.module').then(x => x.MainModule), canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload', relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
