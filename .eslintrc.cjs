module.exports = {
	root: true,
	env: { browser: true, es2020: true },
	settings: {
		react: {
			version: "detect",
		},
	},
	extends: [
		"eslint:recommended",
		"plugin:@typescript-eslint/strict-type-checked",
		"plugin:@typescript-eslint/stylistic-type-checked",
		"plugin:react-hooks/recommended",
		"plugin:react/recommended",
		"plugin:react/jsx-runtime",
	],
	ignorePatterns: ["dist", ".eslintrc.cjs"],
	parser: "@typescript-eslint/parser",
	plugins: ["react-refresh"],
	rules: {
		"react-refresh/only-export-components": [
			"warn",
			{ allowConstantExport: true },
		],
		"@typescript-eslint/no-non-null-assertion": "warn",
		"@typescript-eslint/no-confusing-void-expression": "off",
		"@typescript-eslint/no-unused-vars": "warn",
	},
	parserOptions: {
		ecmaVersion: "latest",
		sourceType: "module",
		project: ["./tsconfig.json", "./tsconfig.node.json"],
		tsConfigRootDir: __dirname,
	}
};
