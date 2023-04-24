import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { NgxImageZoomModule } from 'ngx-image-zoom';


const routes: Routes = [
  { path: 'index', component: LandingPageComponent },
  { path: '', component: LandingPageComponent, pathMatch:'full'}
]
@NgModule({
  declarations: [
    LandingPageComponent
  ],
  imports: [
    //BrowserModule,
    CommonModule,
    FormsModule,
    NgxImageZoomModule,
    RouterModule.forChild(routes),
    NgbModule
  ],
  exports: [RouterModule]
})
export class HomeModule { }
