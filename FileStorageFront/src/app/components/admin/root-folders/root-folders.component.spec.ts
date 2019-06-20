/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { RootFoldersComponent } from './root-folders.component';

describe('RootFoldersComponent', () => {
  let component: RootFoldersComponent;
  let fixture: ComponentFixture<RootFoldersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RootFoldersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RootFoldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
