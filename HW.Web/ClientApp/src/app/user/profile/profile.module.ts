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
const routes: Routes = [
  { path: 'PersonalProfile', component: PersonalProfileComponent, },
  { path: 'PersonalProfileEdit', component: PersonalProfileEditComponent, },
  { path: 'TradesmanProfile', component: TradesmanProfileComponent, },

]



@NgModule({
  declarations: [
    PersonalProfileComponent,
    PersonalProfileEditComponent,
    TradesmanProfileComponent
  ],
  imports: [
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry']
    }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RatingModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    NgxImageCompressService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  
})
export class ProfileModule { }
