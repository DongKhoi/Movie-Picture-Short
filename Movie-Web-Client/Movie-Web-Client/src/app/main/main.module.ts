import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main.component';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//http://domain/login/
export const routes: Routes = [
  //
  { path: '', component: MainComponent},
];

@NgModule({
  declarations: [MainComponent],
  imports: [
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
    FormsModule, 
    RouterModule.forChild(routes)
  ],
})
export class MainModule {}