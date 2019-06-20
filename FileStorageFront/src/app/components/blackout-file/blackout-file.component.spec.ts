/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BlackoutFileComponent } from './blackout-file.component';

describe('BlackoutFileComponent', () => {
  let component: BlackoutFileComponent;
  let fixture: ComponentFixture<BlackoutFileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlackoutFileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlackoutFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
