import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostAdComponent } from './postad/postad.component';
import { NgxImageCompressService } from 'ngx-image-compress';
import { ModalModule } from 'ngx-bootstrap';
const routes: Routes = [
  { path: 'PostAd', component: PostAdComponent, },
]



@NgModule({
  declarations: [
   PostAdComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    ModalModule.forRoot(),
  ],
  providers: [
    NgxImageCompressService
  ],
})
export class PostAdModule { }
