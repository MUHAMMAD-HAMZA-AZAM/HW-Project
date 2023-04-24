import { Component, OnInit } from '@angular/core';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { AccountsService, TodoItemFlatNode, TodoItemNode } from '../accounts.service';
import { FlatTreeControl } from '@angular/cdk/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../Shared/HttpClient/_http';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from "ngx-spinner";
import { httpStatus } from '../../Shared/Enums/enums';


interface Node {
  name: string;
  id: number;
  code: string;
  isControlAccount: boolean;
  children?: Node[];
}

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
  id: number;
  code: string;
  isControlAccount: boolean;
}

var TREE_DATA: Node[] = [];

@Component({
  selector: 'app-chartofaccounttree',
  templateUrl: './chartofaccounttree.component.html',
  styleUrls: ['./chartofaccounttree.component.css'],
  providers: [AccountsService],
})

export class ChartofaccounttreeComponent implements OnInit {
  public subAccountForm: FormGroup;
  public currentNode: FlatNode;
  public parentNode: FlatNode;
  public parentNodeLevel2: FlatNode;
  public parentNodeLevel3: FlatNode;
  public currentlevel: number;
  public disableAddBtn: Boolean = true;
  public disableEditBtn: Boolean = true;
  public disableDeleteBtn: Boolean = true;
  private _transformer = (node: Node, level: number) => {

    let obj= {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      id: node.id,
      code: node.code,
      isControlAccount: node.isControlAccount
    }
    return obj;
   
  }

  treeControl = new FlatTreeControl<FlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor(public _modalService: NgbModal, public fb: FormBuilder, public service: CommonService, public toastr: ToastrService, public Loader: NgxSpinnerService) {
    //this.dataSource.data = TREE_DATA;
  }
  
  hasChild = (_: number, flatnode: FlatNode) => flatnode.expandable;
  getLevel = (node: FlatNode) => node.level;

  Expand(node) {
  }
  ExpandCurrentNodes() {
    
    var index = -1;
    if (this.currentNode.level > 0) {
      if (this.parentNodeLevel2.level > 0) {
        this.treeControl.expand(this.treeControl.dataNodes.find(n => n.id === this.parentNodeLevel3.id && n.name === this.parentNodeLevel3.name));
      }
      this.treeControl.expand(this.treeControl.dataNodes.find(n => n.id === this.parentNodeLevel2.id && n.name === this.parentNodeLevel2.name));
    }
    this.treeControl.expand(this.treeControl.dataNodes.find(n => n.id === this.currentNode.id && n.name === this.currentNode.name));
  }
  
  addNewItem(flatnode: FlatNode, addNewItemModel) {
    debugger
    this.currentNode = flatnode;
    this.currentlevel = this.currentNode.level;
    if (this.currentlevel == 3) {
      this.disableAddBtn = true;
    }
    else {
      this.disableAddBtn = false;
    }
    /////////// For Delete an Item ///////////////////
    if (this.currentlevel == 0 || this.currentNode.isControlAccount == true)
      this.disableDeleteBtn = true;
    else
      this.disableDeleteBtn = false;
    this.disableEditBtn = false;

  }
  AddNewEntry(newAccountContent) {
    this._modalService.open(newAccountContent);
  }
  UpdateEntry(newAccountContent) {
    this.subAccountForm.patchValue(this.currentNode);
    this._modalService.open(newAccountContent);
  }
  DeleteItem(deleteAccountContent) {
    
    this._modalService.open(deleteAccountContent);
  }
  Yes() {
    let formData = this.currentNode;
    this.service.PostData(this.service.apiRoutes.ChartOfAccounts.DeleteChartOfAccounts, JSON.stringify(formData), true).then(result => {
      let responce = result.json();
      this.toastr.error("Data Deleted Successfully", "Delete");
      this.GetCOA();
     
      this._modalService.dismissAll();
      this.disableDeleteBtn = true;
      this.disableEditBtn = true;
      this.disableAddBtn = true;
      this.currentNode = this.getParentNode(this.currentNode);
      if (this.currentNode.level > 0)
        this.parentNodeLevel2 = this.getParentNode(this.currentNode);
      if (this.parentNodeLevel2.level > 0)
        this.parentNodeLevel3 = this.getParentNode(this.parentNodeLevel2);
      var that = this;
      setTimeout(function () { that.ExpandCurrentNodes(); }, 1000);
      
    })
  }
  No() {
    this.disableDeleteBtn = true;
    this.disableEditBtn = true;
    this.disableAddBtn = true;
    this._modalService.dismissAll();
  }
  saveNode()
  {
    let formData = this.subAccountForm.value;
    formData.parentLevel = this.currentNode.level;
    formData.parentIdLevel1 = this.currentNode.id;
    if (this.currentNode.level > 0) {
      formData.parentIdLevel2 = this.getParentNode(this.currentNode).id;
      this.parentNodeLevel2 = this.getParentNode(this.currentNode);
      if (this.parentNodeLevel2.level > 0) {
        formData.parentIdLevel3 = this.getParentNode(this.parentNodeLevel2).id;
        this.parentNodeLevel3 = this.getParentNode(this.parentNodeLevel2);
      }
    }
    
    this.service.PostData(this.service.apiRoutes.ChartOfAccounts.InsertChartOfAccounts, JSON.stringify(formData), true).then(result => {
      let responce = result.json();
      if (responce.status == httpStatus.Ok)
      {
        this.subAccountForm.reset();
        this._modalService.dismissAll();
        this.toastr.success("Data Added Successfully", "Success");
        this.GetCOA();
        this.disableDeleteBtn = true;
        this.disableEditBtn = true;
        this.disableAddBtn = true;
        var that = this;
        setTimeout(function () { that.ExpandCurrentNodes(); }, 1000);
      }
      
      
    })
    
  }

  getParentNode(flatnode: FlatNode): FlatNode | null {
    const currentLevel = this.getLevel(flatnode);

    if (currentLevel < 1) {
      return null;
    }
    const startIndex = this.treeControl.dataNodes.indexOf(flatnode) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const parentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(parentNode) < currentLevel) {
        return parentNode;
      }
    }
  }
  
  

  GetCOA() {
    
    this.Loader.show();
    this.service.get(this.service.apiRoutes.PackagesAndPayments.GetChartOfAccounts).subscribe(result => {
      var responce = result.json();
        var data = JSON.parse(responce);
        TREE_DATA = data.resultData;
        console.log(TREE_DATA);
        this.dataSource.data = TREE_DATA;
    });
      this.Loader.hide();

  }





  ngOnInit() {
    this.GetCOA();
    this.subAccountForm = this.fb.group({
      id:[0],
      name: ['', [Validators.required]],
      code: ['', [Validators.required]],
      isControlAccount: [false],
    })
  }
}
