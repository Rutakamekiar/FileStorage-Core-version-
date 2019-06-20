import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GetFolderContentComponent } from './get-folder-content.component';

describe('GetFoldersComponent', () => {
  let component: GetFolderContentComponent;
  let fixture: ComponentFixture<GetFolderContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [GetFolderContentComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GetFolderContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
