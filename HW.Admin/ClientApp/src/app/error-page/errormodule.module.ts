import { NgModule } from '@angular/core';
import { ErrorPageComponent } from './error-page.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [

  { path:'app-error-page',component: ErrorPageComponent },
];


@NgModule({
  declarations: [
    ErrorPageComponent
  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class ErrormoduleModule { }
