/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FileAdditionButtonsFileComponent } from './file-addition-buttons.component';

describe('FileAdditionButtonsFileComponent', () => {
  let component: FileAdditionButtonsFileComponent;
  let fixture: ComponentFixture<FileAdditionButtonsFileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FileAdditionButtonsFileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FileAdditionButtonsFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
