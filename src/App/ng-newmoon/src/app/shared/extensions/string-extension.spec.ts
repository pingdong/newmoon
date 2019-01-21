import { isNullOrWhitespace } from './string-extension';

describe('StringExtension', () => {

  it('Should return false to a string', () => {
    const target = 'ABC';

    expect(target.isNullOrWhitespace()).toBe(false, 'Non-empty string should return false');
  });

  it('Should return true with a empty string', () => {
    const target = '';

    expect(target.isNullOrWhitespace()).toBe(true, 'Empty string should return true');
  });

  it('Should return true with whitespaces', () => {
    const target = '   ';

    expect(target.isNullOrWhitespace()).toBe(true, 'Empty string should return true');
  });

  it('Should return true with null', () => {
    // tslint:disable-next-line:no-any
    const target: any = null;

    expect(isNullOrWhitespace(target)).toBe(true, 'null should return true');
  });

});
