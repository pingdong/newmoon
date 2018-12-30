import './string-extension';
import { isNullOrWhitespace } from './string-extension';

describe('StringExtension', () => {

  it('Should return false to a string', () => {
    const target = 'ABC';

    expect(target.isNullOrWhitespace()).toBeFalsy();
  });

  it('Should return true with a empty string', () => {
    const target = '';

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

  it('Should return true with whitespaces', () => {
    const target = '   ';

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

  it('Should return true with null', () => {
    // tslint:disable-next-line:no-any
    const target: any = null;

    expect(isNullOrWhitespace(target)).toBeTruthy();
  });

});
