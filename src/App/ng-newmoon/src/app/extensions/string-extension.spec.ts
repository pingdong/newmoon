import './string-extension';

describe('StringExtension', () => {

  it('Non-Empty', () => {
    const target = 'ABC';

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

  it('Empty', () => {
    const target = '';

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

  it('Whitespace', () => {
    const target = '   ';

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

  it('Null', () => {
    const target = null;

    expect(target.isNullOrWhitespace()).toBeTruthy();
  });

});
