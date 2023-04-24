import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { Step1 } from './step1/step1.component';
import { Step2 } from './step2/step2.component';
import { NgxImageCompressService } from 'ngx-image-compress';
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'; 
import { AgmCoreModule } from '@agm/core';
import { googleApiKey } from '../../shared/Enums/enums';
const routes: Routes = [
  { path: 'step1', component: Step1, },
  { path: 'step2', component: Step2, }
]

@NgModule({
  declarations: [Step1, Step2],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    NgxMyDatePickerModule.forRoot(),
    ModalModule.forRoot(),
    NgbModule,
    RouterModule.forChild(routes),
  ],
  providers: [
    NgxImageCompressService
  ],
  exports: [Step2],
  bootstrap: [Step2]
})
export class QuotesModule { }
