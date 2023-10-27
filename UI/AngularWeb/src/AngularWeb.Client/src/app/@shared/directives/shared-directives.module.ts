import { NgModule, Type } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoleBasedAccessControlDirective } from './role-based-access-control.directive';

export const directives: Array<Type<any>> = [
  RoleBasedAccessControlDirective
];

@NgModule({
  declarations: [
    ...directives
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ...directives
  ]
})
export class SharedDirectivesModule { }
