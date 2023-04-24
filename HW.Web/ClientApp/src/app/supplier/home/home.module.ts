import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { BlogsComponent } from './blog/blogs/blogs.component';
import { BlogDetailsComponent } from './blog/blogDetails/blogDetails.component';
import { BlogDetails1Component } from './blog/blogDetails1/blogDetails1.component';
import { BlogDetails2Component } from './blog/blogDetails2/blogDetails2.component';




const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Blogs', component: BlogsComponent, },
  { path: 'BlogDetails', component: BlogDetailsComponent, },
  { path: 'BlogDetails1', component: BlogDetails1Component, },
  { path: 'BlogDetails2', component: BlogDetails2Component, },
]

@NgModule(
  {
    declarations: [
      HomeComponent,
      BlogsComponent,
      BlogDetailsComponent,
      BlogDetails1Component,
      BlogDetails2Component,
    ],
    imports: [
      CommonModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forChild(routes)
    ]
  })
export class HomeModule { }
