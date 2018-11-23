declare global {
  interface String {
    isNullOrWhitespace(): boolean;
  }
}

String.prototype.isNullOrWhitespace = function(): boolean {
  const input = String(this);

  if (!input) {
    return true;
  }

  if (this.trim()) {
    return true;
  } else {
    return false;
  }
};

export {};
