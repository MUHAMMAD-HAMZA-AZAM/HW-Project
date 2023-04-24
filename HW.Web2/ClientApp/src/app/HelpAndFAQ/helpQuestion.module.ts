import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { HelpQuestionComponent } from './helpQuestion/helpQuestion.component';
import { AboutUsComponent } from './aboutUs/aboutUs.component';
import { SupplierhelpquastionComponent } from './supplierhelpquastion/supplierhelpquastion.component';
import { TradesmanhelpquestionComponent } from './tradesmanhelpquestion/tradesmanhelpquestion.component';
import { PrivacypolicyComponent } from './privacypolicy/privacypolicy.component';


const routes: Routes = [
  { path: 'Home/UserFAQs', component: HelpQuestionComponent },
  { path: 'AboutUs', component: AboutUsComponent },
  { path: 'Home/SupplierFAQs', component: SupplierhelpquastionComponent },
  { path: 'Home/TradesmanFAQs', component: TradesmanhelpquestionComponent },
  { path: 'Home/PrivacyPolicy', component: PrivacypolicyComponent },
]

@NgModule({
  declarations: [
    HelpQuestionComponent,
    AboutUsComponent,
    SupplierhelpquastionComponent,
    TradesmanhelpquestionComponent,
    PrivacypolicyComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class HelpQuestionModule { }
