import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartofaccounttreeComponent } from './chartofaccounttree/chartofaccounttree.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule, Routes } from '@angular/router';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JournalEntryComponent } from './journal-entry/journal-entry.component';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { FiscalPeriodComponent } from './fiscal-period/fiscal-period.component';
import { DetailedGlReportComponent } from './detailed-gl-report/detailed-gl-report.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

const routes: Routes = [
  {
    path: 'chartofaccountstree', component: ChartofaccounttreeComponent
  },
  {
    path: 'journalentry', component: JournalEntryComponent
  },
  {
    path: 'fiscalperiod', component: FiscalPeriodComponent
  },
  {
    path: 'glreport', component: DetailedGlReportComponent
  },
]

@NgModule({
  declarations: [
    ChartofaccounttreeComponent,
    JournalEntryComponent,
    FiscalPeriodComponent,
    DetailedGlReportComponent],
  imports: [
    CommonModule,
    MatTreeModule,
    MatIconModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    AutocompleteLibModule,
    NgMultiSelectDropDownModule.forRoot(),
  ],
  exports: [RouterModule],
})
export class AccountsModule { }
