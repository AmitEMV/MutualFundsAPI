
CREATE TABLE amc(
id INT NOT NULL AUTO_INCREMENT,
`name` VARCHAR(128),
 PRIMARY KEY (id)
);

CREATE TABLE amc_funds(
id INT NOT NULL AUTO_INCREMENT,
amc_id INT NOT NULL,
`mfapiId` INT NOT NULL,
`fund_name` VARCHAR(256),
`fund_type` VARCHAR(64),
 PRIMARY KEY (id),
 FOREIGN KEY (amc_id) REFERENCES amc(id)
);

CREATE TABLE amc_fund_nav(
id BIGINT NOT NULL AUTO_INCREMENT,
amc_fund_id INT NOT NULL,
`date` DATE NOT NULL,
nav DOUBLE NOT NULL,
PRIMARY KEY (id),
FOREIGN KEY (amc_fund_id) REFERENCES amc_funds(id)
);

-- creating amcs
INSERT INTO amc(`name`) VALUES 
('Axis Mutual Fund'),
('Mirae Asset Mutual Fund'),
('PPFAS Mutual Fund'),
('HDFC Mutual Fund'),
('UTI Mutual Fund'),
('Quantum Mutual Fund'),
('SBI Mutual Fund');

INSERT INTO amc_funds(amc_id,`mfapiId`,`fund_name`,`fund_type`) VALUES
(1,120465,'Axis Bluechip Fund - Direct Plan - Growth','Equity Scheme - Large Cap Fund'),
(2,118834,'Mirae Asset Emerging Bluechip Fund - Direct Plan - Growth','Equity Scheme - Large & Mid Cap Fund'),
(2,118825,'Mirae Asset Large Cap Fund - Direct Plan - Growth','Equity Scheme - Multi Cap Fund'),
(3,143269,'Parag Parikh Liquid Fund- Direct Plan- Growth','Debt Scheme - Liquid Fund'),
(3,122639,'Parag Parikh Long Term Equity Fund - Direct Plan - Growth','Equity Scheme - Multi Cap Fund'),
(4,119063,'HDFC Index Fund-Nifty 50 Plan-Direct Plan','Other Scheme - Index Funds'),
(5,143341,'UTI Nifty Next 50 Index Fund - Direct Plan - Growth Option','Other Scheme - Index Funds'),
(6,103734,'Quantum Liquid Fund - Direct Plan Growth Option','Debt Scheme - Liquid Fund');
