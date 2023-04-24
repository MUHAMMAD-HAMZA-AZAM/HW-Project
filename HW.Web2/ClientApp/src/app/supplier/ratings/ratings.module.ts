import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { RatingModule } from 'ng-starrating';



const routes: Routes = [
  { path: 'List', component: ListComponent },
]

@NgModule(
  {
    declarations: [
      ListComponent],
    imports: [
      CommonModule,
      HttpClientModule,
      RatingModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes)
    ]
  })
export class RatingsModule { }
