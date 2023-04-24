import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserAgreementComponent } from './userAgreement/userAgreement.component';
import { TradesmanagreementComponent } from './tradesmanagreement/tradesmanagreement.component';
import { SupplieragreementComponent } from './supplieragreement/supplieragreement.component';


const routes: Routes = [
  { path: 'UserAgreement', component: UserAgreementComponent, },
  { path: 'TradesmanAgreement', component: TradesmanagreementComponent, },
  { path: 'SupplierAgreement', component: SupplieragreementComponent, }
]

@NgModule({
  declarations: [
    UserAgreementComponent,
    TradesmanagreementComponent,
    SupplieragreementComponent,
    

  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class AgrementsModule { }
