import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserAgreementComponent } from './userAgreement/userAgreement.component';


const routes: Routes = [
  { path: 'UserAgreement', component: UserAgreementComponent, }
]

@NgModule({
  declarations: [
    UserAgreementComponent,

  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AgrementsModule { }
