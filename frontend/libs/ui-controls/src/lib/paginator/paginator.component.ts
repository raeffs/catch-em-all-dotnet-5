import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { PaginatedDataSource } from '@raeffs/data-source';

@Component({
  selector: 'cea-paginator',
  templateUrl: 'paginator.component.html',
  styleUrls: ['paginator.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginatorComponent {
  @Input()
  public dataSource: PaginatedDataSource<unknown> | null = null;

  public nextPage(): void {
    this.dataSource?.changeToNextPage();
  }

  public prevPage(): void {
    this.dataSource?.changeToPreviousPage();
  }
}
