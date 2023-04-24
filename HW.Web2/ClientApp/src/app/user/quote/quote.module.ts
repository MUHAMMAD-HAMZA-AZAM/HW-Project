import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxImageCompressService } from 'ngx-image-compress';
//import { AmazingTimePickerModule } from 'amazing-time-picker';
import { QuoteListComponent } from './list/list.component';
import { QuoteDetailComponent } from './detail/detail.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
const routes: Routes = [
  { path: 'List', component: QuoteListComponent, },
  { path: 'Detail', component: QuoteDetailComponent, }
]

@NgModule({
  declarations: [
    QuoteListComponent,
    QuoteDetailComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AutocompleteLibModule,
    NgbModule,
    ModalModule.forRoot(),
    RouterModule.forChild(routes)
  ],
  providers: [
    NgxImageCompressService,
   
  ],
})
export class QuoteModule { }
