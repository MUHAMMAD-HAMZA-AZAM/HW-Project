import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { MyBidsListComponent } from './list/list.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


const routes: Routes = [
  { path: 'List', component: MyBidsListComponent, },

]

@NgModule({
  declarations: [
    MyBidsListComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    InfiniteScrollModule,
    RouterModule.forChild(routes)
  ]
})
export class MyBidsModule { }
