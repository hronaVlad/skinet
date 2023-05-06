import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {

  @Input() label: string;
  @Input() type = 'text';
  @ViewChild('input', {static: true}) input: ElementRef;

  constructor(@Self() public controlDirective: NgControl) { 
    this.controlDirective.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDirective.control;

    control.setValidators(control.validator ? [control.validator] : []);
    control.setAsyncValidators(control.asyncValidator ? [control.asyncValidator] : []);
    control.updateValueAndValidity();
  }

  onChange(value: any):void { }
  onTouched(): void { }

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    throw new Error('Method not implemented.');
  }
}
