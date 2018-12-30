import { DyanmicFormTranslateService } from './dynamic-form.translate.service';
import { SelectionItem } from '../models/selection.item';
import { TextItem } from '../models/text.item';

describe('DyanmicFormTranslateService', () => {

  let service: DyanmicFormTranslateService;

  beforeEach(() => { service = new DyanmicFormTranslateService(); });

  it('Empty setting should return empty controls', () => {
    const array  = [];

    const group = service.toFormGroup(array);

    // tslint:disable-next-line:no-magic-numbers
    expect(Object.keys(group.controls).length).toEqual(0);
  });

  it('Null should throw an exception', () => {
    expect(function() { service.toFormGroup(null); }).toThrowError('argument is null');
  });

  it('Should return correct controls', () => {
    const array  = [
      new SelectionItem({key: 'first', value: 'stringvalue1'}),
      new TextItem({key: 'second', value: 'stringvalue2'})
    ];

    const group = service.toFormGroup(array);

    // tslint:disable-next-line:no-magic-numbers
    expect(Object.keys(group.controls).length).toEqual(2);

    expect(group.controls['first'].value).toEqual('stringvalue1');
    expect(group.controls['second'].value).toEqual('stringvalue2');
  });

  it('Should return controls in order', () => {
    const array  = [
      new SelectionItem({key: 'first', value: 'stringvalue1', order: 2}),
      new TextItem({key: 'second', value: 'stringvalue2', order: 1})
    ];

    const group = service.toFormGroup(array);

    // tslint:disable-next-line:no-magic-numbers
    expect(Object.keys(group.controls).length).toEqual(2);

    expect(group.controls[Object.keys(group.controls)[0]].value).toEqual('stringvalue2');
    expect(group.controls[Object.keys(group.controls)[1]].value).toEqual('stringvalue1');
  });

  it('Should return controls with correct validators', () => {
    const array  = [
      new SelectionItem({key: 'first', value: 'stringvalue1', required: true}),
      new TextItem({key: 'second', value: 'stringvalue2' })
    ];

    const group = service.toFormGroup(array);

    // tslint:disable-next-line:no-magic-numbers
    expect(Object.keys(group.controls).length).toEqual(2);

    expect(group.controls['first'].validator).toBeDefined();
    expect(group.controls['second'].validator).toBeNull();
  });

  it('Should return empty string when no value provided', () => {
    const array  = [
      new SelectionItem({key: 'first' }),
      new TextItem({key: 'second', required: true })
    ];

    const group = service.toFormGroup(array);

    // tslint:disable-next-line:no-magic-numbers
    expect(Object.keys(group.controls).length).toEqual(2);

    expect(group.controls['first'].value).toEqual('');
    expect(group.controls['second'].value).toEqual('');
  });

});
