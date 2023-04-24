import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveleadsComponent } from './liveleads.component';
import { Routes, RouterModule } from '@angular/router';
import { AgmCoreModule } from '@agm/core';
import { googleApiKey } from '../../shared/Enums/enums';
import { TradesmanlistComponent } from '../tradesmanlist/tradesmanlist.component';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
const routes: Routes = [
  {
    path: '',component: LiveleadsComponent,
  }
  
  ]

@NgModule({
  declarations: [LiveleadsComponent],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    HttpClientModule,
    NgbModule,
    RouterModule.forChild(routes),
    AgmCoreModule.forRoot({
      apiKey: googleApiKey.mapApiKey,
      libraries: ['places', 'geometry', 'name'],
      
    }),
    
    
  ]
})
export class LiveleadsModule { }
