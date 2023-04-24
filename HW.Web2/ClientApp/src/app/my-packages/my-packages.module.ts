import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MypackagesComponent } from './mypackages/mypackages.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'mypackages', component: MypackagesComponent },
]


@NgModule({
  declarations: [MypackagesComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class MyPackagesModule { }
