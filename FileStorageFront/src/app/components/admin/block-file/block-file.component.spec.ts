/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { BlockFileComponent } from './block-file.component';

describe('BlockFileComponent', () => {
  let component: BlockFileComponent;
  let fixture: ComponentFixture<BlockFileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BlockFileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BlockFileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
