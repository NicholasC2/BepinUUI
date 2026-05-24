# MADE BY CHATGPT cus i can't be bothered

import os

SOURCE_DIR = "."
OUTPUT_FILE = "dist/UniversalUI.cs"

IGNORE_DIRS = {
    "bin",
    "obj",
    "dist",
    ".git",
    ".vs",
    "__pycache__"
}

using_lines = set()
source_chunks = []

for root, dirs, files in os.walk(SOURCE_DIR):

    dirs[:] = [
        d for d in dirs
        if d not in IGNORE_DIRS
    ]

    files.sort()

    for file in files:

        if not file.endswith(".cs"):
            continue

        path = os.path.join(root, file)

        print(f"Bundling: {path}")

        with open(path, "r", encoding="utf-8") as f:
            content = f.read()

        filtered_lines = []

        for line in content.splitlines():

            stripped = line.strip()

            if stripped.startswith("using "):
                using_lines.add(stripped)
                continue

            filtered_lines.append(line)

        source_chunks.append("\n".join(filtered_lines))

os.makedirs(os.path.dirname(OUTPUT_FILE), exist_ok=True)

final_output = []

for using in sorted(using_lines):
    final_output.append(using)

final_output.append("")

final_output.extend(source_chunks)

with open(OUTPUT_FILE, "w", encoding="utf-8") as f:
    f.write("\n\n".join(final_output))

print(f"\nBundled successfully -> {OUTPUT_FILE}")