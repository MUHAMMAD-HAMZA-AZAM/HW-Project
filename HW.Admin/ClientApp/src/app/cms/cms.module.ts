import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AngularEditorModule } from '@kolkov/angular-editor';
//import { NgDragDropModule } from 'ng-drag-drop';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { AddpostComponent } from './addpost/addpost.component';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';  
import { AddcategoryComponent } from './addcategory/addcategory.component';
import { CreatepostComponent } from './createpost/createpost.component';
import { AddsubcategoryComponent } from './addsubcategory/addsubcategory.component';
import { PostsComponent } from './posts/posts.component';
import { PostdetailsComponent } from './postdetails/postdetails.component';
import { TrasformhtmlPipe } from './trasformhtml.pipe';
import { EditpostComponent } from './editpost/editpost.component';
import { TagInputModule } from 'ngx-chips';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PageseoComponent } from './pageseo/pageseo.component';


const routes: Routes = [
  { path: 'addblogpost', component: AddpostComponent },
  { path: 'addcategory', component: AddcategoryComponent },
  { path: 'addsubcategory', component: AddsubcategoryComponent },
  { path: 'createpost', component: CreatepostComponent },
  { path: 'postslist', component: PostsComponent },
  { path: 'postdetails/:id', component: PostdetailsComponent },
  { path: 'editpost/:id', component: EditpostComponent },
  { path: 'pageseo', component: PageseoComponent },
];

@NgModule({
  declarations: [AddpostComponent, AddcategoryComponent, CreatepostComponent, AddsubcategoryComponent, PostsComponent, PostdetailsComponent, TrasformhtmlPipe, EditpostComponent, PageseoComponent],
  imports: [
    CommonModule,
    AngularEditorModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    //NgDragDropModule.forRoot(),
    NgMultiSelectDropDownModule,
    TagInputModule,
    FormsModule,
    NgbModule,
  ],
  exports: [RouterModule],
})
export class CMSModule { }
