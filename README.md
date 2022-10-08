## Install jhipster:
```bash
npm install -g generator-jhipster
```

Update

```bash
npm install -g generator-jhipster
```

Use older version of jHipster

```bash
npm install -g generator-jhipster@7.8.1
```

## Install dotnetcore blueprint:

```bash
npm install -g generator-jhipster-dotnetcore
```

To update this blueprint:

```bash
npm update -g generator-jhipster-dotnetcore
```

## Generate code

### Create base solution

```bash
jhipster --blueprints dotnetcore
```

### Add entities to base solution

With cli

```bash
jhipster entity <entity-name>
```

or with jdl (https://start.jhipster.tech/jdl-studio/)

```bash
jhipster import-jdl my_file.jdl
```

Example:
```bash
cd src
jhipster --blueprints dotnetcore
#..Doing configurations..
jhipster import-jdl ../docs/jhipster-jdl.jdl
```