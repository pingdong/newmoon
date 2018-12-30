declare global {
  interface String {
    isNullOrWhitespace(): boolean;
  }
}

String.prototype.isNullOrWhitespace = function(): boolean {
  return isNullOrWhitespace(this);
};

export function isNullOrWhitespace(input) {
  if (typeof input === 'undefined' || input == null) {
    return true;
  }

  if (!input) {
    return true;
  }

  return !input.trim();
}

export {};
