import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HelpQuestionComponent } from './helpQuestion/helpQuestion.component';
import { AboutUsComponent } from './aboutUs/aboutUs.component';


const routes: Routes = [
  { path: 'Home/UserFAQs', component: HelpQuestionComponent },
  { path: 'Common/AboutUs', component: AboutUsComponent }
]

@NgModule({
  declarations: [
    HelpQuestionComponent,
    AboutUsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class HelpModule { }
