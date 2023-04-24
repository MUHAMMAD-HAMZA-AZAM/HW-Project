import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PersonalProfileComponent } from './personalProfile/personalProfile.component';
import { PersonalProfileEditComponent } from './personalProfileEdit/personalProfileEdit.component';
import { TradesmanProfileComponent } from './tradesmanProfile/tradesmanProfile.component';
import { RatingModule } from 'ng-starrating';
import { NgxImageCompressService } from 'ngx-image-compress';
import { googleApiKey } from '../../shared/Enums/enums';
import { AgmCoreModule } from '@agm/core';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
const routes: Routes = [
  { path: 'PersonalProfile', component: PersonalProfileComponent, },
  { path: 'PersonalProfileEdit', component: PersonalProfileEditComponent, },
  { path: 'TradesmanProfile', component: TradesmanProfileComponent, },

]



@NgModule({
  declarations: [
    PersonalProfileComponent,
    PersonalProfileEditComponent,
    TradesmanProfileComponent,

  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ImageCropperModule,
    ReactiveFormsModule,
    RatingModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
    NgbModule,


  ],
  providers: [
    NgxImageCompressService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  
})
export class ProfileModule { }
