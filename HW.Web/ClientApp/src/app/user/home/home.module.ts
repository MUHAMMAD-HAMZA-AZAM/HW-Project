import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogsComponent } from './blogs/blogs.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { BlogDetailsComponent } from './blogDetails/blogDetails.component';
import { BlogDetails2Component } from './blogDetails2/blogDetails2.component';
import { BlogDetails1Component } from './blogDetails1/blogDetails1.component';
const routes: Routes = [
  { path: 'Blogs', component: BlogsComponent, },
  { path: 'BlogDetails', component: BlogDetailsComponent, },
  { path: 'BlogDetails1', component: BlogDetails1Component, },
  { path: 'BlogDetails2', component: BlogDetails2Component, },
 // { path: 'UserAgreement', component: UserAgreementComponent, }
  
]

@NgModule({
  declarations: [
    BlogsComponent,
    BlogDetailsComponent,
    BlogDetails1Component,
    BlogDetails2Component,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes)
  ]
})
export class HomeModule { }
